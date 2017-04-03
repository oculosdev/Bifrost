﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Applications;
using Bifrost.Execution;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="ApplicationResourceResolverFor{T}"/> that
    /// knows how to resolve <see cref="IEvent">events</see>
    /// </summary>
    public class EventApplicationResourceResolver : ApplicationResourceResolverFor<EventApplicationResourceType>
    {
        IApplication _application;
        IEnumerable<Type> _eventTypes;

        /// <summary>
        /// Initializes a new instance of <see cref="EventApplicationResourceResolver"/>
        /// </summary>
        /// <param name="application">Current <see cref="IApplication"/></param>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> for finding correct <see cref="IEvent">event type</see></param>
        public EventApplicationResourceResolver(IApplication application, ITypeDiscoverer typeDiscoverer)
        {
            _application = application;
            _eventTypes = typeDiscoverer.FindMultiple<IEvent>();
        }

        /// <inheritdoc/>
        public override Type Resolve(IApplicationResourceIdentifier identifier)
        {
            var formats = _application.Structure.GetStructureFormatsForArea(ApplicationAreas.Events);
            var types = _eventTypes.Where(t => t.Name == identifier.Resource.Name);

            foreach (var type in types)
            {
                foreach (var format in formats)
                {
                    if (format.Match(type.Namespace).HasMatches) return type;
                }
            }

            throw new UnknownApplicationResourceType(identifier.Resource.Type.Identifier);
        }
    }
}
