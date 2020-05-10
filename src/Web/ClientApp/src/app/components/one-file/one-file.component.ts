import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "eg-one-file",
  templateUrl: "./one-file.component.html",
  styleUrls: ["./one-file.component.scss"],
})
export class OneFileComponent {
  @Output() fileDropped = new EventEmitter<File>();

  @Input() disabled = false;
  @Input() required = false;
  @Input() formControlName!: string;

  fileName: string = null;

  addFile(files: FileList | null): void {
    if (!files) {
      return;
    }
    const f = files.item(0) as File;
    this.fileDropped.emit(f);
    this.fileName = f.name;
  }
}
