import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Department } from "../models/Department";
import { Employee } from "../models/Employee";
import { DepartmentService } from "../service/department.service";
import { EmployeeService } from "../service/employee.service";
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit {

  public title = 'Departamentos';
  public departmentForm!: FormGroup
  public employeeForm!: FormGroup
  public departments!: Department[];
  public employees!: Employee[];
  public selectedDepartment?: Department;
  public selectedEmployee?: Employee;
  public newDepIsActive: boolean = false;
  public newEmpIsActive: boolean = false;
  public expandIsActive: boolean = false;
  public editEmpIsActive: boolean = false;

  constructor(private fb: FormBuilder,
    private departmentService: DepartmentService,
    private employeeService: EmployeeService,
    public sanitizer: DomSanitizer) {
    this.createDepForm();
    this.createEmpForm();
  }

  ngOnInit(): void {
    this.loadDepartments();
  }


  selectDepartment(department: Department) {
    this.selectedDepartment = department
    this.departmentForm.patchValue(department)
  }

  selectEmployee(employee: Employee) {
    this.selectedEmployee = employee
    this.employeeForm.patchValue(employee)
  }

  unselectDepartment() {
    this.selectedDepartment = undefined;
  }

  unselectEmployee() {
    this.selectedEmployee = undefined;
  }

  createDepForm() {
    this.departmentForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      abbrev: ['', Validators.required],
    });
  }

  createEmpForm() {
    this.employeeForm = this.fb.group({
      id!: [''],
      name: ['', Validators.required],
      rg: ['', Validators.required],
      departmentId: [''],
      picture: [null]
    })
  }

  loadDepartments() {
    this.departmentService.get().subscribe(
      (departments: Department[]) => {
        this.departments = departments
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  loadEmployees(department: Department) {
    this.employeeService.getByDepartment(department.id).subscribe(
      (employees: Employee[]) => {
        this.employees = employees
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  editDepartment(department: Department) {
    this.departmentService.put(department.id, department).subscribe(
      (department: Department) => {
        console.log(department);
        this.loadDepartments();
        this.unselectDepartment();
      },
      (error: any) => {
        console.log(error)
      }
    )
  }

  editEmployee(editedEmployee: Employee) {
    var department: Department
    this.departmentService.getById(editedEmployee.departmentId).subscribe(
      (departmentModel: Department) => {
        department = departmentModel
      }
    )

    var formData: any = new FormData();
    formData.append('name', this.employeeForm.get('name')?.value);
    formData.append('RG', this.employeeForm.get('rg')?.value);
    formData.append('departmentId', this.employeeForm.get('departmentId')?.value);
    formData.append('picture', this.employeeForm.get('picture')?.value);

    this.employeeForm.patchValue(editedEmployee)
    this.employeeService.put(editedEmployee.id, formData).subscribe(
      (employee: Employee) => {
        this.loadEmployees(department);
        this.unselectEmployee();
      },
      (error: any) => {
        console.error(error);
      }
    )
  }

  submmitEditedDepartment() {
    console.log(this.departmentForm.value)
    this.editDepartment(this.departmentForm.value)
  }

  uploadFile(event: any) {
    const file = event.target.files[0];
    this.employeeForm.patchValue({
      picture: file,
    });
    console.log(this.employeeForm.value)
    //this.employeeForm.get('picture').updateValueAndValidity();
  }

  deleteDepartment(department: Department) {
    this.selectedDepartment = department
    this.departmentForm.patchValue(department)
    this.departmentService.delete(department.id).subscribe(
      (department: Department) => {
        console.log(department);
        this.loadDepartments()
      }
    )
    this.unselectDepartment();
  }

  deleteEmployee(deletedEmployee: Employee) {
    var department: Department
    this.departmentService.getById(deletedEmployee.departmentId).subscribe(
      (departmentModel: Department) => {
        department = departmentModel
      }
    )
    this.employeeService.delete(deletedEmployee.id).subscribe(
      (employee: Employee) => {
        this.loadEmployees(department);
        this.unselectEmployee();
      },
      (error: any) => {
        console.error(error);
      }
    )
  }

  createDepartment() {
    const department: Department = this.departmentForm.value
    this.departmentForm.patchValue(department)
    this.departmentService.post(department).subscribe(
      (department: Department) => {
        console.log(department);
        this.loadDepartments()
      },
      (error: any) => {
        console.error(error)
      }
    )
    this.newDepIsActive = false;
  }

  createEmployee(department: Department) {

    var formData: any = new FormData();
    formData.append('name', this.employeeForm.get('name')?.value);
    formData.append('RG', this.employeeForm.get('rg')?.value);
    formData.append('departmentId', this.selectedDepartment?.id);
    formData.append('picture', this.employeeForm.get('picture')?.value);

    this.employeeService.post(formData).subscribe(
      (employee: Employee) => {
        console.log(employee);
        this.loadEmployees(employee.department)
      },
      (error: any) => {
        console.error(error)
      }
    )
  }
}