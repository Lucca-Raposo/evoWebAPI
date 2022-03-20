import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  public title = 'Funcionários';

  public employees = [
    { Id: 1, Name: "Lucca", DepartmentId: 5, RG: 533210872 },
    { Id: 2, Name: "Pedro", DepartmentId: 5, RG: 533210872 },
    { Id: 3, Name: "João", DepartmentId: 6, RG: 533210872 }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
