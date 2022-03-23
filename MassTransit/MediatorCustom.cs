using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.Clients;
using MassTransit.Configuration;
using MassTransit.Context;
using MassTransit.Mediator;
using MassTransit.Pipeline;
using MassTransit.Transports;

namespace MassTransitShared
{
    public class MediatorCustom : IMediator
    {
        private IMediator _mediator;
        public MediatorCustom(MassTransitMediator mediator) { _mediator = mediator; }
        public ClientFactoryContext Context => _mediator.Context;
        public ConnectHandle ConnectConsumeMessageObserver<T>(IConsumeMessageObserver<T> observer) where T : class => _mediator.ConnectConsumeMessageObserver(observer);
        public ConnectHandle ConnectConsumeObserver(IConsumeObserver observer) => _mediator.ConnectConsumeObserver(observer);
        public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe) where T : class => _mediator.ConnectConsumePipe<T>(pipe);
        public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe, ConnectPipeOptions options) where T : class => _mediator.ConnectConsumePipe<T>(pipe, options);
        public ConnectHandle ConnectPublishObserver(IPublishObserver observer) => _mediator.ConnectPublishObserver(observer);
        public ConnectHandle ConnectRequestPipe<T>(Guid requestId, IPipe<ConsumeContext<T>> pipe) where T : class => _mediator.ConnectRequestPipe<T>(requestId, pipe);
        public ConnectHandle ConnectSendObserver(ISendObserver observer) => _mediator.ConnectSendObserver(observer);
        public RequestHandle<T> CreateRequest<T>(T message, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(message, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(Uri destinationAddress, T message, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(destinationAddress, message, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, T message, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(consumeContext, message, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, Uri destinationAddress, T message, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(consumeContext, destinationAddress, message, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(object values, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(values, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(Uri destinationAddress, object values, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(destinationAddress, values, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, object values, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(consumeContext, values, cancellationToken, timeout);
        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, Uri destinationAddress, object values, CancellationToken cancellationToken = default, RequestTimeout timeout = default) where T : class => _mediator.CreateRequest<T>(consumeContext ,destinationAddress, values, cancellationToken, timeout);
        public IRequestClient<T> CreateRequestClient<T>(RequestTimeout timeout = default) where T : class
        {
            return default;
        }
        public IRequestClient<T> CreateRequestClient<T>(ConsumeContext consumeContext, RequestTimeout timeout = default) where T : class => _mediator.CreateRequestClient<T>(consumeContext, timeout);
        public IRequestClient<T> CreateRequestClient<T>(Uri destinationAddress, RequestTimeout timeout = default) where T : class => _mediator.CreateRequestClient<T>(destinationAddress, timeout);
        public IRequestClient<T> CreateRequestClient<T>(ConsumeContext consumeContext, Uri destinationAddress, RequestTimeout timeout = default) where T : class => _mediator.CreateRequestClient<T>(consumeContext, destinationAddress, timeout);
        public ValueTask DisposeAsync() => _mediator.DisposeAsync();

        public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(message, cancellationToken);
        public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(message, publishPipe, cancellationToken);
        public Task Publish<T>(T message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(message, publishPipe, cancellationToken);
        public Task Publish(object message, CancellationToken cancellationToken = default) => _mediator.Publish(message, cancellationToken);
        public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) => _mediator.Publish(message, publishPipe, cancellationToken);
        public Task Publish(object message, Type messageType, CancellationToken cancellationToken = default) => _mediator.Publish(message, messageType, cancellationToken);
        public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) => _mediator.Publish(message, publishPipe, cancellationToken);
        public Task Publish<T>(object values, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(values, cancellationToken);
        public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(values,publishPipe, cancellationToken);
        public Task Publish<T>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = default) where T : class => _mediator.Publish<T>(values, publishPipe, cancellationToken);
        public Task Send<T>(T message, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T>(message, cancellationToken);
        public Task Send<T>(T message, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T>(message, pipe, cancellationToken);
        public Task Send<T>(T message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T> (pipe, cancellationToken);
        public Task Send(object message, CancellationToken cancellationToken = default) => _mediator.Send(message, cancellationToken);
        public Task Send(object message, Type messageType, CancellationToken cancellationToken = default) => _mediator.Send(message, messageType, cancellationToken);
        public Task Send(object message, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) => _mediator.Send(message, pipe, cancellationToken);
        public Task Send(object message, Type messageType, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) => _mediator.Send(message, messageType, pipe, cancellationToken);
        public Task Send<T>(object values, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T>(values, cancellationToken);
        public Task Send<T>(object values, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T>(values, pipe, cancellationToken);
        public Task Send<T>(object values, IPipe<SendContext> pipe, CancellationToken cancellationToken = default) where T : class => _mediator.Send<T>(values, pipe, cancellationToken);
    }
}
