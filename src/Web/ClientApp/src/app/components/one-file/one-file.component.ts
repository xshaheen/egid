import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "eg-one-file",
  templateUrl: "./one-file.component.html",
  styleUrls: ["./one-file.component.scss"],
})
export class OneFileComponent {
  @Output() oneFile = new EventEmitter<File>();

  @Input() disabled = false;
  @Input() required = false;
  @Input() formControlName!: string;

  fileName: string = null;

  addFile(files: FileList | null): void {
    if (!files) {
      return;
    }
    const f = files.item(0) as File;
    this.oneFile.emit(f);
    this.fileName = f.name;
  }
}
