using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Cqrs.Command
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Result Execute(TCommand command);
    }
}
