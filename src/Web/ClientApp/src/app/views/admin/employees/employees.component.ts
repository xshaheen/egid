import { Component } from "@angular/core";
import { DataSource } from "src/app/services/data-source.service";
import { ICitizen } from "src/app/models/citizen.model";
import { MatTableDataSource } from "@angular/material/table";

@Component({
  templateUrl: "./employees.component.html",
  styleUrls: ["./employees.component.scss"],
})
export class EmployeesComponent {
  constructor(private data: DataSource) {}

  getEmployees(): MatTableDataSource<ICitizen> {
    const employees: ICitizen[] = [];
    this.data.getEmployees().subscribe((e) => employees.push(e));
    return new MatTableDataSource(employees);
  }
}
