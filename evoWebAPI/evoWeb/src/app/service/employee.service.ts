import { HttpClient } from '@angular/common/http';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseUrl = `${environment.baseUrl}/employees`

  constructor(private http: HttpClient) { }

  //get employees by department
  getByDepartment(departmentId: number): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseUrl}/${departmentId}`)
  }

  post(employee: FormData): Observable<Employee> {
    return this.http.post<Employee>(`${this.baseUrl}`, employee)
  }

  put(id: number, employee: FormData): Observable<Employee> {
    return this.http.put<Employee>(`${this.baseUrl}/${id}`, employee)
  }

  delete(id: number): Observable<Employee> {
    return this.http.delete<Employee>(`${this.baseUrl}/${id}`)
  }
}
