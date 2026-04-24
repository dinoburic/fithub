export interface FilterOption {
  label: string;
  value: string|number;
  checked: boolean;
}

export interface FilterGroup {
  title: string;
  icon: string; 
  type: 'checkbox' | 'radio' | 'range';
  options: FilterOption[];
}