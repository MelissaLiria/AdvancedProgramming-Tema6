﻿using Application.Abstract;
using Domain.Entities.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Samples.Queries.GetAverageOfSampleDoubles
{
    public record GetAverageOfSampleDoublesQuery(IEnumerable<SampleDouble> SampleDoubles) : IQuery<Double>
    {
    }
}