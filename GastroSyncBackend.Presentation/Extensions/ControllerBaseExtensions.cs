using GastroSyncBackend.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult ApiResponse<T>(this ControllerBase controller, bool success, string message, T? data)
    {
        if (controller == null) throw new ArgumentNullException(nameof(controller));
        return new JsonResult(new ApiResponse<T?>
        {
            Success = success,
            Message = message,
            Data = data
        });
    }
}