using Sqids;

namespace WebApi.Mvc;

public class SqidsRouteConstraint(SqidsEncoder<int> sqidsEncoder) : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value))
            return sqidsEncoder.Decode(Convert.ToString(value)).Count > 0;

        return false;
    }
}