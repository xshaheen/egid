import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "eg-file",
  templateUrl: "./file.component.html",
  styleUrls: ["./file.component.scss"],
})
export class FileComponent {
  @Output() fileDropped = new EventEmitter<File>();

  @Input() disabled = false;
  @Input() required = false;
  @Input() formControlName!: string;

  isHovering: boolean;

  onHovering(event: boolean) {
    this.isHovering = event;
  }

  addFile(files: FileList | null) {
    if (!files) {
      return;
    }
    for (let i = 0; i < files.length; i++) {
      this.fileDropped.emit(files.item(i) as File);
    }
  }
}
