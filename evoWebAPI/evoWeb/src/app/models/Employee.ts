import { Department } from "./Department";

export class Employee {
    id!: number
    name!: string
    rg!: number
    picture!: string;
    departmentId!: number
    department!: Department;
}
