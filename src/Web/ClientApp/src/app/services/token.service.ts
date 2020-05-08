import { Injectable } from "@angular/core";

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
}
