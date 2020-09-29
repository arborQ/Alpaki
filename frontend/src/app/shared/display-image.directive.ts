import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[appDisplayImage]'
})
export class DisplayImageDirective {
  @Input() appDisplayImage: string;

  constructor(private el: ElementRef) {
    alert(this.appDisplayImage);
  }
}
