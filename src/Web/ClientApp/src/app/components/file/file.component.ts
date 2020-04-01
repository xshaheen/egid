import { Component, Input, Output, EventEmitter } from "@angular/core";

export class FileModel {
  file: File;
  progress: number;
}

@Component({
  selector: "eg-file",
  templateUrl: "./file.component.html",
  styleUrls: ["./file.component.scss"]
})
export class FileComponent {
  @Output() fileDropped = new EventEmitter<FileModel>();

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
      const f = files.item(i) as File;
      this.fileDropped.emit({ file: f, progress: 0 });
    }
  }
}
