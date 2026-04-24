import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FilterGroup, FilterOption } from '../../models/filter.model';

@Component({
  selector: 'app-filter-group',
  templateUrl: './filter-group.component.html',
  styleUrls: ['./filter-group.component.scss'],
  standalone: false
})
export class FilterGroupComponent {
  @Input() group!: FilterGroup;
  @Output() changed = new EventEmitter<void>();

  isExpanded = true;

  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }

  onToggle(option: FilterOption) {
    option.checked = !option.checked;
    this.changed.emit(); 
  }
}