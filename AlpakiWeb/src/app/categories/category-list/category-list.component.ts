import { Component } from '@angular/core';
import { CategoriesService, ICategory } from '../categories.service';
import { map, switchMap } from 'rxjs/operators';
import { Subject, ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.less']
})
export class CategoryListComponent {

  constructor(private categoriesService: CategoriesService) {
  }

  categoriesResponse$ = this.categoriesService.getCategories();
  categories$ = this.categoriesResponse$.pipe(map(response => response.data.categories));
  isLoading$ = this.categoriesResponse$.pipe(map(response => response.loading));

  submitUpdateCategory({ category }: { category: ICategory }) {
    this.categoriesService.updateCategory(category);
  }

  submitCreateNewCategory({ category }: { category: ICategory }) {
    this.categoriesService.addCategory(category);
  }
}
