import { Component, OnInit } from '@angular/core';
import { Apollo, QueryRef } from 'apollo-angular';
import { initFlowbite } from 'flowbite';
import { delay, retryWhen, take, tap } from 'rxjs';
import { Todo, TodoList } from 'src/Models/todoModels';
import {
  ADD_TODO,
  CHECK_TODO,
  DELETE_ALL_TODOS,
  DELETE_TODO,
  GET_TODOS,
} from 'src/constants/gqlConsts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  appTitle = 'My to do list 📝';

  todos: Todo[] = [];
  loading = true;
  error: any;

  toDoQuery!: QueryRef<TodoList>;

  constructor(private apollo: Apollo) {}

  ngOnInit() {
    initFlowbite();

    this.toDoQuery = this.apollo.watchQuery<TodoList>({
      query: GET_TODOS,
    });

    this.toDoQuery.valueChanges
      .pipe(
        retryWhen((errors) =>
          errors.pipe(
            tap((e) => console.log('retry: ' + e)),
            delay(4000),
            take(5)
          )
        )
      )
      .subscribe((result) => {
        this.todos = result.data?.todos;
        this.loading = result.loading;
        // TODO : Error handling
        this.error = result.error;
      });
  }

  deleteAll() {
    this.apollo.mutate({ mutation: DELETE_ALL_TODOS }).subscribe((res) => {
      this.updateToDos();
    });
  }

  deleteItem(item: Todo) {
    this.apollo
      .mutate({
        mutation: DELETE_TODO,
        variables: {
          todoId: item.id,
        },
      })
      .subscribe((res) => {
        this.updateToDos();
      });
  }

  checkItem(item: Todo) {
    this.apollo
      .mutate({
        mutation: CHECK_TODO,
        variables: {
          todoId: item.id,
        },
      })
      .subscribe((res) => {
        this.updateToDos();
      });
  }

  onSubmit(form: HTMLFormElement, e: SubmitEvent) {
    e.preventDefault();
    e.stopPropagation();
    let title = (form.elements[0] as HTMLInputElement).value;
    let item = (form.elements[1] as HTMLInputElement).value;

    this.apollo
      .mutate({
        mutation: ADD_TODO,
        variables: {
          item,
          title,
        },
      })
      .subscribe((res) => {
        this.updateToDos();
      });
    (form.elements[0] as HTMLInputElement).value = '';
    (form.elements[1] as HTMLInputElement).value = '';
  }

  updateToDos() {
    this.toDoQuery.refetch();
  }
}
