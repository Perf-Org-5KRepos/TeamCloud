﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using FluentValidation;
using TeamCloud.Model.Internal.Data;
using TeamCloud.Model.Validation;

namespace TeamCloud.Model.Internal.Validation.Data
{
    public sealed class ProviderValidator : AbstractValidator<Provider>
    {
        public ProviderValidator()
        {
            RuleFor(obj => obj.Id).MustBeProviderId();
            RuleFor(obj => obj.Url).MustBeUrl();
            RuleFor(obj => obj.AuthCode).MustBeFunctionAuthCode();
        }
    }
}
