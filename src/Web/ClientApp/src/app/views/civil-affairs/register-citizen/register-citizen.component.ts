import { Component } from "@angular/core";

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: "eg-register-citizen",
  templateUrl: "./register-citizen.component.html",
  styleUrls: ["./register-citizen.component.scss"],
})
export class RegisterCitizenComponent {
  favoriteSeason: string;
  seasons: string[] = ["Winter", "Spring", "Summer", "Autumn"];

  foods: Food[] = [
    { value: "steak-0", viewValue: "Steak" },
    { value: "pizza-1", viewValue: "Pizza" },
    { value: "tacos-2", viewValue: "Tacos" },
  ];
}
