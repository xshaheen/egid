import {
  Directive,
  EventEmitter,
  HostListener,
  Output,
  Input
} from "@angular/core";

@Directive({
  selector: "[egDropZone]"
})
export class DropZoneDirective {
  @Output() filesDropped = new EventEmitter<FileList>();
  @Output() hovered = new EventEmitter<boolean>();

  @Input("egDropZone") disabled = false;

  @HostListener("dragover", ["$event"])
  onDragOver($event: DragEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    if (!this.disabled) {
      this.hovered.emit(true);
    }
  }

  @HostListener("dragleave", ["$event"])
  onDragLeave($event: DragEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    if (!this.disabled) {
      this.hovered.emit(false);
    }
  }

  @HostListener("drop", ["$event"])
  onDrop($event: DragEvent) {
    $event.preventDefault();
    $event.stopPropagation();
    if (!this.disabled) {
      this.hovered.emit(false);
      const files = $event.dataTransfer.files;
      if (files.length > 0) {
        this.filesDropped.emit(files);
      }
    }
  }
}
