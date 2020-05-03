using System;
using System.Collections.Generic;
using Autofac;
using Mobile.Client.ViewModels;
using Xamarin.Forms;

namespace Mobile.Client.Factories
{
    public class ViewFactory : IViewFactory
    {
        private readonly IDictionary<Type, Type> map = new Dictionary<Type, Type>();
        private readonly IComponentContext componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : Page
        {
            map[typeof(TViewModel)] = typeof(TView);
        }

        public void Register<TViewModel, TData, TView>() where TViewModel : class, IViewModel<TData> where TView : Page
        {
            map[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>() where TViewModel : class, IViewModel
        {
            return ResolveInternal(out TViewModel _);
        }

        public Page Resolve<TViewModel>(out TViewModel viewModel) where TViewModel : class, IViewModel
        {
            return ResolveInternal(out viewModel);
        }

        public Page Resolve<TViewModel, TData>(out TViewModel viewModel) where TViewModel : class, IViewModel<TData>
        {
            return ResolveInternal(out viewModel);
        }

        private Page ResolveInternal<TViewModel>(out TViewModel viewModel) where TViewModel : class
        {
            viewModel = componentContext.Resolve<TViewModel>();
            var viewType = map[typeof(TViewModel)];
            var resolved = componentContext.Resolve(viewType);
            if (!(resolved is Page view)) return default;

            view.BindingContext = viewModel;
            return view;
        }
    }
}
