import { Component, Input } from "@angular/core";

@Component({
  selector: "eg-qr-writer",
  templateUrl: "qr-writer.component.html",
  styleUrls: ["./qr-writer.component.scss"]
})
export class QrWriterComponent {
  @Input() content: string;
}
