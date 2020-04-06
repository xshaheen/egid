import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ICitizen } from "../models/citizen.model";
import { Observable, from } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class DataSource {
  citizens = CITIZENS;

  getEmployees(): Observable<ICitizen> {
    return from(this.citizens);
  }

  getCitizens(): Observable<ICitizen> {
    return from(this.citizens);
  }
}

const CITIZENS: ICitizen[] = [
  {
    id: "123",
    fullName: "مجمود حمدي شاهين",
    Email: "mxshaheen@outlook.com",
    address: "كوم حمادة البحيرة",
    dateOfBirth: new Date("15/3/1998"),
    gender: "ذكر",
    phoneNumber: "01000000000",
    photoUrl:
      "https://lh3.googleusercontent.com/oTgWsSm-Zd6YVenWYr0e3hBL0bIIKyPTLUo_G3Qm4b9eYy1DjLN17x8r4R-6NQVasP0rd_PmThNnx5S-GQ=s220-rw",
  },
  {
    id: "124",
    fullName: "جودي حمدي شاهين",
    Email: "jody@outlook.com",
    address: "كوم حمادة البحيرة",
    dateOfBirth: new Date("14/2/2014"),
    gender: "انثي",
    phoneNumber: "01100000000",
    photoUrl: "",
  },
  {
    id: "124",
    fullName: "ايمان السعيد غازي",
    Email: "eman@outlook.com",
    address: "طنطا الغربية",
    dateOfBirth: new Date("14/2/2000"),
    gender: "انثي",
    phoneNumber: "01100000000",
    photoUrl: "",
  },
  {
    id: "124",
    fullName: "اسراء مصطفي العبد",
    Email: "israa@outlook.com",
    address: "طنطا الغربية",
    dateOfBirth: new Date("14/2/2000"),
    gender: "انثي",
    phoneNumber: "01100000000",
    photoUrl: "",
  },
];
