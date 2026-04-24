// src/app/core/components/base-list.component.ts

import {BaseComponent} from './base-component';

export abstract class BaseListComponent<TItem> extends BaseComponent{
  items: TItem[] = [];

  /**
   * Konkretnu implementaciju punjenja podataka ostavljamo djeci.
   */
  protected abstract loadData(): void;

  /**
   * Helper that can be called from a child component's ngOnInit.
   */
  protected initList(): void {
    this.loadData();
  }
}
