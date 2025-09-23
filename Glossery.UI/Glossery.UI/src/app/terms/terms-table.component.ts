import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TermsService, TermDto } from '../services/terms.service';

@Component({
  selector: 'app-terms-table',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './terms-table.component.html'
})
export class TermsTableComponent implements OnInit {
  private api = inject(TermsService);

  terms: TermDto[] = [];
  newName = '';
  newDefinition = '';

  message: string | null = null;
  messageType: 'success' | 'error' | null = null;

  ngOnInit(): void { this.reload(); }

  reload(): void {
    this.api.list().subscribe(x => this.terms = x);
  }

  showMessage(text: string, type: 'success' | 'error') {
    this.message = text;
    this.messageType = type;
    setTimeout(() => { this.message = null; this.messageType = null; }, 5000);
  }

  add(): void {
    const name = this.newName.trim();
    const def  = this.newDefinition.trim();
    if (!name || !def) return;

    this.api.create(name, def).subscribe({
      next: () => {
        this.newName = '';
        this.newDefinition = '';
        this.reload();
        this.showMessage('Term added successfully.', 'success');
      },
      error: (err) => {
        this.showMessage(err?.error?.error ?? err?.error?.message ?? err?.message ?? 'Error adding term.', 'error');
      }
    });
  }

  publish(t: TermDto): void {
    this.api.publish(t.id).subscribe({
      next: () => { this.reload(); this.showMessage('Term published.', 'success'); },
      error: (err) => {
        this.showMessage(err?.error?.error ?? err?.error?.message ?? err?.message ?? 'Error publishing.', 'error');
      }
    });
  }

  archive(t: TermDto): void {
    this.api.archive(t.id).subscribe({
      next: () => { this.reload(); this.showMessage('Term archived.', 'success'); },
      error: (err) => {
        this.showMessage(err?.error?.error ?? err?.error?.message ?? err?.message ?? 'Error archiving.', 'error');
      }
    });
  }

  remove(t: TermDto): void {
    this.api.delete(t.id).subscribe({
      next: () => { this.reload(); this.showMessage('Term deleted.', 'success'); },
      error: (err) => {
        this.showMessage(err?.error?.error ?? err?.error?.message ?? err?.message ?? 'Error deleting.', 'error');
      }
    });
  }
}
