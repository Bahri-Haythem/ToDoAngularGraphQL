export interface TodoList {
  todos: Todo[];
}

export interface Todo {
  __typename: string;
  title: string;
  item: string;
  isDone: boolean;
  id: number;
}
