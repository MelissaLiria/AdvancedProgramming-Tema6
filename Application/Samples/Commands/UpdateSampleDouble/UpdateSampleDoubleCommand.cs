﻿using Application.Abstract;
using Domain.Entities.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Samples.Commands.UpdateSampleDouble
{
    public record UpdateSampleDoubleCommand(SampleDouble SampleDouble) : ICommand;
}
