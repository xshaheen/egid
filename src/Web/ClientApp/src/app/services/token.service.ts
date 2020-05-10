import { Injectable } from "@angular/core";
import * as jwt_decode from "jwt-decode";

@Injectable({ providedIn: "root" })
export class AppTokenService {
  private storage = sessionStorage;
  private TOKEN = "token";

  clear(): void {
    this.storage.removeItem(this.TOKEN);
  }

  any(): boolean {
    return this.get() !== null;
  }

  get(): string | null {
    return this.storage.getItem(this.TOKEN);
  }

  set(token: string): void {
    this.storage.setItem(this.TOKEN, token);
  }

  decode(key: string) {
    const token = this.get();
    if (!token) {
      return null;
    }
    const decodedToken = jwt_decode(token);
    if (!decodedToken) {
      return null;
    }
    const value = decodedToken[key];

    if (value === undefined) {
      return null;
    }
    return value;
  }
}
