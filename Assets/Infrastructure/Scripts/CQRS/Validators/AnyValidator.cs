﻿using System.Collections.Generic;

namespace Assets.Infrastructure.Scripts.CQRS.Validators {
	public class AnyValidator<TMessage> : BaseConditionValidator<TMessage> where TMessage : class, IMessage {
		private readonly List<IMessageConditionValidator> _validators;

		public AnyValidator(List<IMessageConditionValidator> validators) {
			_validators = validators;
		}
		public override ValidationResult Validate(IMessageHandler handler, TMessage message) {
			var count = _validators.Count;
			for (var i = 0; i < count; ++i) {
				var validator = _validators[i];
				if (validator.Validate(handler, message) == ValidationResult.Accepted) {
					return ValidationResult.Accepted;
				}
			}

			return ValidationResult.Rejected;
		}
	}
}