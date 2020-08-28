import { Component, OnInit, Input } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ProgressSpinnerMode } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-dream-progress',
  templateUrl: './dream-progress.component.html',
  styleUrls: ['./dream-progress.component.less']
})
export class DreamProgressComponent implements OnInit {

  @Input() imageId: string;
  @Input() progressValue: number;
  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'determinate';
  constructor() { }

  ngOnInit(): void {
  }

}
