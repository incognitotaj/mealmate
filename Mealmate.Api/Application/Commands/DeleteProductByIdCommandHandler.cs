﻿using System.Threading;
using System.Threading.Tasks;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using MediatR;

namespace Mealmate.Api.Application.Commands
{
    public class DeleteRestaurantByIdCommandHandler : IRequestHandler<DeleteByIdRequest>
    {
        private readonly IRestaurantService _restaurantService;

        public DeleteRestaurantByIdCommandHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<Unit> Handle(DeleteByIdRequest request, CancellationToken cancellationToken)
        {
            await _restaurantService.Delete(request.Id);

            return Unit.Value;
        }
    }
}
