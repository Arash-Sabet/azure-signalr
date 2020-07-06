﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Connections;
using Microsoft.Azure.SignalR.Protocol;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.SignalR.IntegrationTests.MockService;

namespace Microsoft.Azure.SignalR.IntegrationTests.Infrastructure
{
    internal class MockServiceConnectionFactory : ServiceConnectionFactory
    {
        IMockService _mockService;
        public MockServiceConnectionFactory(
            IMockService mockService,
            IServiceProtocol serviceProtocol,
            IClientConnectionManager clientConnectionManager,
            IConnectionFactory connectionFactory,
            ILoggerFactory loggerFactory,
            ConnectionDelegate connectionDelegate,
            IClientConnectionFactory clientConnectionFactory,
            IServerNameProvider nameProvider)
            : base(
                  serviceProtocol,
                  clientConnectionManager,
                  new MockServiceConnectionContextFactory(mockService), // use instead of connectionFactory
                  loggerFactory,
                  connectionDelegate,
                  clientConnectionFactory,
                  nameProvider)
        {
            _mockService = mockService;
        }

        public override IServiceConnection Create(HubServiceEndpoint endpoint, IServiceMessageHandler serviceMessageHandler, ServiceConnectionType type)
        {
            var serviceConnection = base.Create(endpoint, serviceMessageHandler, type);
            return new MockServiceConnection(_mockService, serviceConnection);
        }
    }
}
