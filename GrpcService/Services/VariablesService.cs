﻿using Contracts;
using Contracts.Variables;
using EnvironmentalVariablesDAQ.GrpcProtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using AutoMapper;
using MediatR;
using System.Reflection.Metadata.Ecma335;
using Application.Variables.Commands.CreateVariable;
using Domain.Entities.ConfigurationData;
using Application.Variables.Queries.GetVariableById;

namespace GrpcService.Services
{
    public class VariablesService : EnvironmentalVariablesDAQ.GrpcProtos.Variable.VariableBase
    {
        private readonly IMediator _mediator;

        public VariablesService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task<VariableDTO> CreateVariable(CreateVariableRequest request, ServerCallContext context)
        {
            Structure location;

            switch ((int)request.LocationCase)
            {
                case 4:
                    location = new Building(
                        new Guid(request.Building.Id),
                        request.Building.Address,
                        request.Building.Number);
                    break;
                case 5:
                    location = new Floor(
                        new Guid(request.Floor.Id),
                        request.Floor.Location,
                        new Building(
                            new Guid(request.Floor.Building.Id),
                            request.Floor.Building.Address,
                            request.Floor.Building.Number));
                    break;
                default:
                    location = new Room(
                        new Guid(request.Room.Id),
                        request.Room.Number,
                        request.Room.IsProduction,
                        request.Room.Description,
                        new Floor(
                            new Guid(request.Floor.Id),
                            request.Room.Floor.Location,
                            new Building(
                                new Guid(request.Room.Floor.Building.Id),
                                request.Room.Floor.Building.Address,
                                request.Room.Floor.Building.Number)));
                    break;
            }
            
            var command = new CreateVariableCommand(
                location,
                new Domain.ValueObjects.VariableType(
                    request.VariableType.Name,
                    request.VariableType.MeasurementUnit),
                request.Code);

            var result = _mediator.Send(command).Result;
        }

        public override Task<Empty> DeleteVariable(DeleteRequest request, ServerCallContext context)
        {
            return base.DeleteVariable(request, context);
        }

        public override Task<Variables> GetAllVariables(Empty request, ServerCallContext context)
        {
            return base.GetAllVariables(request, context);
        }

        public override Task<NullableVariableDTO> GetVariable(GetRequest request, ServerCallContext context)
        {
            var query = new GetVariableByIdQuery(new Guid(request.Id));

            var result = _mediator.Send(query).Result;

        }

        public override Task<Empty> UpdateVariable(VariableDTO request, ServerCallContext context)
        {
            return base.UpdateVariable(request, context);
        }

    }
}