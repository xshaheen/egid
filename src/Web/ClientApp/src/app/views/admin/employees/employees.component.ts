import { Component } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";

@Component({
  templateUrl: "./employees.component.html",
  styleUrls: ["./employees.component.scss"],
})
export class EmployeesComponent {
  // constructor(private data: DataSource) {}
  // getEmployees(): MatTableDataSource<ICitizen> {
  //   const employees: ICitizen[] = [];
  //   this.data.getEmployees().subscribe((e) => employees.push(e));
  //   return new MatTableDataSource(employees);
  // }
}
