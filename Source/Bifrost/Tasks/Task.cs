﻿#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.Collections.Generic;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a task that can run and can potentially be paused, resumed and persisted
    /// </summary>
    public abstract class Task
    {
        /// <summary>
        /// Gets or sets the current operation the task is on
        /// </summary>
        public int CurrentOperation { get; set; }

        /// <summary>
        /// <see cref="TaskId">Identifier</see> of the task
        /// </summary>
        public TaskId Id { get; set; }

        /// <summary>
        /// Get the operations for the task
        /// </summary>
        public abstract TaskOperation[] Operations { get; }

        /// <summary>
        /// Gets wether or not operations can run asynchronously, default is true
        /// </summary>
        /// <remarks>
        /// Override this to change the default behavior of it running everything asynchronously
        /// </remarks>
        public virtual bool CanRunOperationsAsynchronously { get { return true; } }

        /// <summary>
        /// Gets called when the task is about to begin
        /// </summary>
        public virtual void Begin() { }

        /// <summary>
        /// Gets called when the task is ended, meaning when all the operations are done
        /// </summary>
        public virtual void End() { }

        /// <summary>
        /// Gets a boolean telling if the task is done or not
        /// </summary>
        /// <returns></returns>
        public bool IsDone { get { return CurrentOperation >= Operations.Length; } }
    }
}
