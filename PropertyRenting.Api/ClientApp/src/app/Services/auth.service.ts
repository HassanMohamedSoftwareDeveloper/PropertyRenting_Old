import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { IUser } from "../Models/iuser";
import { Login } from "../Models/login";
import { Register } from "../Models/register";
import { JwtHelperService } from "@auth0/angular-jwt";
import { User } from "../Models/user";
import { ChangePassword } from "../Models/change-password";
import { UpdateUser } from "../Models/update-user";
import { ResetUserPassword } from "../Models/reset-user-password";

@Injectable({
    providedIn: "root",
})
export class AuthService {
    roles = ["Admin", "SubAdmin", "User"];

    currentUser: IUser = { username: "", email: "", role: "" };

    helper = new JwtHelperService();

    constructor(private http: HttpClient) {}

    loggOut() {
        localStorage.removeItem("token");
        this.currentUser = { username: "", email: "", role: "" };
    }
    Login(model: Login): Observable<IUser> {
        return this.http
            .post(environment.ApiURL + "api/v1/auth/login", model)
            .pipe(
                map((response: any) => {
                    const decodedToken = this.helper.decodeToken(
                        response.token
                    );

                    this.currentUser.username = decodedToken.given_name;
                    this.currentUser.email = decodedToken.email;
                    this.currentUser.role = decodedToken.role;

                    localStorage.setItem("token", response.token);
                    return this.currentUser;
                })
            );
    }
    Register(model: Register) {
        return this.http.post(
            environment.ApiURL + "api/v1/auth/register",
            model
        );
    }
    GetAll(): Observable<User[]> {
        return this.http.get<User[]>(environment.ApiURL + "api/v1/auth/users");
    }
    GetUserById(userId: string): Observable<User> {
        return this.http.get<User>(
            environment.ApiURL + "api/v1/auth/users/" + userId
        );
    }
    ChangePassword(request: ChangePassword) {
        return this.http.post(
            environment.ApiURL + "api/v1/auth/change-password",
            request
        );
    }
    LoggedIn() {
        const token = localStorage.getItem("token") || "";
        const islogged = !this.helper.isTokenExpired(token);
        if (islogged) {
            const decodedToken = this.helper.decodeToken(token);

            this.currentUser.username = decodedToken.given_name;
            this.currentUser.email = decodedToken.email;
            this.currentUser.role = decodedToken.role;
        }
        return islogged;
    }
    UserName(): string {
        return this.currentUser.username;
    }
    IsAdmin(): boolean {
        return this.LoggedIn() && this.currentUser.role === "Admin";
    }
    IsSubAdmin(): boolean {
        return this.LoggedIn() && this.currentUser.role === "SubAdmin";
    }
    IsUser(): boolean {
        return this.LoggedIn() && this.currentUser.role === "User";
    }
    UpdateUser(model: UpdateUser) {
        return this.http.post(
            environment.ApiURL + "api/v1/auth/update-user",
            model
        );
    }
    ResetUserPassword(model: ResetUserPassword) {
        return this.http.post(
            environment.ApiURL + "api/v1/auth/reset-user-password",
            model
        );
    }
}
