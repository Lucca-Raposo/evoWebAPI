import { HttpClient } from '@angular/common/http';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Department } from '../models/Department';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {

  baseUrl = `${environment.baseUrl}/departments`

  constructor(private http: HttpClient) { }

  //Get All Departments
  get(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.baseUrl}`)
  }

  getById(id: number): Observable<Department> {
    return this.http.get<Department>(`${this.baseUrl}/${id}`)
  }

  put(id: number, department: Department): Observable<Department> {
    return this.http.put<Department>(`${this.baseUrl}/${id}`, department)
  }

  delete(id: number): Observable<Department> {
    return this.http.delete<Department>(`${this.baseUrl}/${id}`)
  }

  post(department: Department): Observable<Department> {
    return this.http.post<Department>(`${this.baseUrl}`, department)
  }

}
