import { Injectable } from "@angular/core";
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpHeaders,
} from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class AuthHeaderInterceptor implements HttpInterceptor {
    intercept(
        request: HttpRequest<unknown>,
        next: HttpHandler
    ): Observable<HttpEvent<unknown>> {
        request = request.clone({
            headers: new HttpHeaders({
                Authorization: "Bearer " + localStorage.getItem("token"),
                "Accept-Language": localStorage.getItem("lang") || "",
            }),
        });

        return next.handle(request);
    }
}
