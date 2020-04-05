import { Component } from "@angular/core";
import { DataSource } from "src/app/services/data-source.service";
import { ICitizen } from "src/app/models/citizen.model";

@Component({
  templateUrl: "./employees.component.html",
  styleUrls: ["./employees.component.scss"],
})
export class EmployeesComponent {
  constructor(private data: DataSource) {}

  getEmployees(): ICitizen[] {
    const employees: ICitizen[] = [];
    this.data.getEmployees().subscribe((e) => employees.push(e));
    return employees;
  }
}
