import { gql } from 'apollo-angular';

export const GET_TODOS = gql`
  query GetTodos {
    todos {
      title
      item
      isDone
      id
    }
  }
`;
export const DELETE_ALL_TODOS = gql`
  mutation deleteAllTodos {
    deleteAll
  }
`;
export const DELETE_TODO = gql`
  mutation DeleteTodo($todoId: Int!) {
    deleteToDo(id: $todoId)
  }
`;
export const ADD_TODO = gql`
  mutation AddTodo($item: String, $title: String!) {
    addToDo(input: { item: $item, title: $title }) {
      id
      title
      isDone
    }
  }
`;
export const CHECK_TODO = gql`
  mutation CheckTodo($todoId: Int!) {
    checkToDo(id: $todoId) {
      id
      isDone
    }
  }
`;
