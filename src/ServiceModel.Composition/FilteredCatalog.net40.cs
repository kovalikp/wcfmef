#if NET40

namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    internal class FilteredCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
    {
        private readonly Func<ComposablePartDefinition, bool> _filter;
        private ComposablePartCatalog _innerCatalog;
        private volatile bool _disposed;
        private object _lock = new object();

        public FilteredCatalog(ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter)
        {
            _filter = filter;
            _innerCatalog = catalog;
            INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._innerCatalog as INotifyComposablePartCatalogChanged;
            if (notifyComposablePartCatalogChanged != null)
            {
                notifyComposablePartCatalogChanged.Changed += new EventHandler<ComposablePartCatalogChangeEventArgs>(this.OnChangedInternal);
                notifyComposablePartCatalogChanged.Changing += new EventHandler<ComposablePartCatalogChangeEventArgs>(this.OnChangingInternal);
            }
        }

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

        public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                return _innerCatalog.Parts.Where(x => _filter(x));
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !this._disposed)
                {
                    INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = null;
                    try
                    {
                        lock (this._lock)
                        {
                            if (!this._disposed)
                            {
                                this._disposed = true;
                                notifyComposablePartCatalogChanged = this._innerCatalog as INotifyComposablePartCatalogChanged;
                                this._innerCatalog = null;
                            }
                        }
                    }
                    finally
                    {
                        if (notifyComposablePartCatalogChanged != null)
                        {
                            notifyComposablePartCatalogChanged.Changed -= new EventHandler<ComposablePartCatalogChangeEventArgs>(this.OnChangedInternal);
                            notifyComposablePartCatalogChanged.Changing -= new EventHandler<ComposablePartCatalogChangeEventArgs>(this.OnChangingInternal);
                        }
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
        {
            EventHandler<ComposablePartCatalogChangeEventArgs> changed = this.Changed;
            if (changed != null)
            {
                changed(this, e);
            }
        }

        protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
        {
            EventHandler<ComposablePartCatalogChangeEventArgs> changing = this.Changing;
            if (changing != null)
            {
                changing(this, e);
            }
        }

        private void OnChangedInternal(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = this.ProcessEventArgs(e);
            if (composablePartCatalogChangeEventArgs != null)
            {
                this.OnChanged(this.ProcessEventArgs(composablePartCatalogChangeEventArgs));
            }
        }

        private void OnChangingInternal(object sender, ComposablePartCatalogChangeEventArgs e)
        {
            ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = this.ProcessEventArgs(e);
            if (composablePartCatalogChangeEventArgs != null)
            {
                this.OnChanging(this.ProcessEventArgs(composablePartCatalogChangeEventArgs));
            }
        }

        private ComposablePartCatalogChangeEventArgs ProcessEventArgs(ComposablePartCatalogChangeEventArgs e)
        {
            ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = new ComposablePartCatalogChangeEventArgs(e.AddedDefinitions.Where(this._filter), e.RemovedDefinitions.Where(this._filter), e.AtomicComposition);
            if (composablePartCatalogChangeEventArgs.AddedDefinitions.Any<ComposablePartDefinition>() || composablePartCatalogChangeEventArgs.RemovedDefinitions.Any<ComposablePartDefinition>())
            {
                return composablePartCatalogChangeEventArgs;
            }

            return null;
        }
    }
}

#endif