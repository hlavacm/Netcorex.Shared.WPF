using System;
using Netcorex.Shared.WPF.Models;

namespace Netcorex.Shared.WPF.ViewModels
{
  /// <summary>
  /// The common basis for ViewModels
  /// </summary>
  /// <typeparam name="T">Generic type of <see cref="ModelBase"/></typeparam>
  public abstract class ViewModelBase<T> : IViewModel
    where T : ModelBase
  {
    private readonly T _model;


    protected ViewModelBase(T model)
    {
      if (model == null)
        throw new ArgumentNullException("model");
      _model = model;
    }

    public T Model
    {
      get { return _model; }
    }
    IModel IViewModel.Model
    {
      get { return _model; }
    }
  }
}
