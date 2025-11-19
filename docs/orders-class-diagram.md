<!-- Mermaid class diagram for OrdersController and MediatR commands/handlers (updated) -->
```mermaid
classDiagram
    class OrdersController {
        - IMediator _mediator
        + Get() : Task<ActionResult<IEnumerable<Order>>>
        + Get(id: Guid) : Task<ActionResult<Order>>
        + Post(order: Order) : Task<ActionResult>
        + Put(id: Guid, order: Order) : Task<ActionResult>
        + Delete(id: Guid) : ActionResult
    }

    class Order {
        + Guid Id
        + string CustomerName
        + List<string> Items
        + string Status
    }

    class GetOrdersQuery {
        + Guid OrderId
    }

    class CreateOrderCommand {
        + string CustomerName
        + List<string> Items
    }

    class UpdateOrderCommand {
        + Guid OrderId
        + string CustomerName
        + List<string> Items
    }

    class GetOrdersHandler {
        + Handle(request: GetOrdersQuery) : Task<List<Order>>
    }

    class CreateOrderHandler {
        + Handle(request: CreateOrderCommand) : Task<Guid>
    }

    class UpdateOrderHandler {
        + Handle(request: UpdateOrderCommand) : Task<Guid>
    }

    class IMediator {
        + Send(request) : Task
    }

    class IRequest {
        <<interface>>
    }

    class IRequestHandler {
        <<interface>>
    }

    OrdersController --> IMediator : uses

    %% Commands/Queries implement MediatR IRequest<T> (not IMediator)
    IRequest <|.. GetOrdersQuery
    IRequest <|.. CreateOrderCommand
    IRequest <|.. UpdateOrderCommand

    %% Handlers implement MediatR IRequestHandler<TRequest,TResponse>
    IRequestHandler <|.. GetOrdersHandler
    IRequestHandler <|.. CreateOrderHandler
    IRequestHandler <|.. UpdateOrderHandler

    %% Handlers handle respective requests
    GetOrdersHandler --> GetOrdersQuery : handles
    CreateOrderHandler --> CreateOrderCommand : handles
    UpdateOrderHandler --> UpdateOrderCommand : handles

    %% Handlers interact with Order model
    GetOrdersHandler --> Order : returns List<Order>
    CreateOrderHandler --> Order : creates and returns Id
    UpdateOrderHandler --> Order : creates/updates and returns Id
