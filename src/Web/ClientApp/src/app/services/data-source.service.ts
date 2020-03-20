import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

const PORT = 52663;

@Injectable({
  providedIn: "root"
})
export class DataSource {
  constructor(private http: HttpClient) {
    this.baseUrl = `http//${location.hostname}:${PORT}`;
  }

  baseUrl: string;
}
