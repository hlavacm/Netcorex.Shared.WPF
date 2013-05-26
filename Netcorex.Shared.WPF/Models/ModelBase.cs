using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Netcorex.Shared.WPF.Localization;
using Netcorex.Shared.WPF.Common;

namespace Netcorex.Shared.WPF.Models
{
	/// <summary>
  /// The common basis for Models including validation issues
	/// </summary>
	public abstract class ModelBase : BindableBase, IModel
	{
    private static readonly string DefaultValidationErrorText = SharedTitles.ValidationError;
		private readonly Dictionary<string, Func<ModelBase, object>> _propertyGetters;
		private readonly Dictionary<string, ValidationAttribute[]> _validators;


		protected ModelBase()
		{
			_validators = GetType()
			  .GetProperties()
			  .Where(p => GetValidations(p).Length != 0)
			  .ToDictionary(p => p.Name, GetValidations);

			_propertyGetters = GetType()
			  .GetProperties()
			  .Where(p => GetValidations(p).Length != 0)
			  .ToDictionary(p => p.Name, GetValueGetter);
		}


		public string this[string propertyName]
		{
			get
			{
				string result = null;
				if (_propertyGetters.ContainsKey(propertyName))
				{
					object propertyValue = _propertyGetters[propertyName](this);
					List<string> errors = new List<string>();
					foreach (var validator in _validators[propertyName])
					{
						if (validator.IsValid(propertyValue))
						{
							continue;
						}
						errors.Add(validator.ErrorMessage ?? DefaultValidationErrorText);
					}
					result = string.Join(Environment.NewLine, errors);
				}
				OnPropertyChanged("IsValid");
				return result;
			}
		}
		public string Error
		{
			get
			{
				string[] errors = (from validator in _validators
								   from attribute in validator.Value
								   where !attribute.IsValid(_propertyGetters[validator.Key](this))
								   select attribute.ErrorMessage ?? DefaultValidationErrorText).ToArray();
				if (errors.Length == 0)
				{
					return null;
				}
				return string.Join(Environment.NewLine, errors);
			}
		}
		public bool IsValid
		{
			get { return Error == null; }
		}



		public bool IsPropertyValid(string propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				return string.IsNullOrEmpty(Error);
			}
			return string.IsNullOrEmpty(this[propertyName]);
		}


		private ValidationAttribute[] GetValidations(PropertyInfo property)
		{
			if (property == null)
			{
				return null;
			}
			return property.GetCustomAttributes(typeof(ValidationAttribute), true) as ValidationAttribute[];
		}

		private Func<ModelBase, object> GetValueGetter(PropertyInfo property)
		{
			return x => property.GetValue(x, null);
		}
	}
}