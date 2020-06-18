﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Common.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
