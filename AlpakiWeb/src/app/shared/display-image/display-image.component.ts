import { Component, OnInit, Input, ContentChildren } from '@angular/core';
import { ImagesService } from '../../images.service';

@Component({
  selector: 'app-display-image',
  templateUrl: './display-image.component.html',
  styleUrls: ['./display-image.component.less']
})
export class DisplayImageComponent implements OnInit {

  @Input() fileId: string;
  @Input() onFileChanged: (fileId: string) => void;
  file: string | ArrayBuffer;
  get canUploadFile() { return !this.onFileChanged; }
  constructor(private imageService: ImagesService) { }

  ngOnInit(): void {
    this.imageService.getFile(this.fileId).then(file => {
      this.file = file;
    });
  }
}
