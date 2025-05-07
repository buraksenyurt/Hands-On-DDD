namespace App.Framework;

public interface IApplicationService
{
    Task Handle(object command);
}
