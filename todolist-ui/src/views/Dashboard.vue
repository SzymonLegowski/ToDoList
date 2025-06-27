<template>
  <AppHeader
    :username="username"
    @logout="logout"
  ></AppHeader>
  <component
    :is="selectedComponent"
    :todoitem="todoitem"
    :todoItems="todoitems"
    :isEditMode="isEditMode"
    @create="createTodoitem"
    @edit="editTodoitem"
    @delete="deleteTodoitem"
    @Submit="submitForm"
    @Cancel="component='list'">
  </component>
</template>
<script setup>
import { ref, onMounted, inject, computed } from 'vue';
import { useAuthStore } from '@/stores/auth';
import { useRouter } from 'vue-router';
import TodoItemForm from '@/components/TodoItemForm.vue';
import TodoItemList from '@/components/TodoItemList.vue';
import AppHeader from '@/components/AppHeader.vue';
import emitter from '@/eventBus';
const auth = useAuthStore();
const router = useRouter();
const username = localStorage.getItem('username');
const todoitem = ref(null);
const todoitems = ref([]);
const component = ref("list");
const TodoitemRepository = inject('TodoitemRepository');
const isEditMode = ref(false);

onMounted(async () => {
    todoitems.value = await TodoitemRepository.getAll(); 
    console.log("students",todoitems.value);
});
const selectedComponent = computed(() => {
    return component.value === "list" ? TodoItemList : TodoItemForm;
});

function createTodoitem(){
  todoitem.value = null;
  component.value = "editor";
  isEditMode.value = false;
}

function editTodoitem(tdi){
  todoitem.value = tdi;
  component.value = "editor";
  isEditMode.value = true;
}

async function deleteTodoitem(tdi){
  await TodoitemRepository.delete(tdi.id);
  let index = todoitems.value.findIndex(t => t.id === tdi.id);
  todoitem.value = null;
  todoitems.value.splice(index, 1);
  emitter.emit('alert', {
      msg: "Usunięto zadanie",
       variant: 'info'
  })
}

async function submitForm(tdi){
  if(todoitem.value?.id)
  {
    await TodoitemRepository.update(todoitem.value.id, tdi);
    let index = todoitems.value.findIndex(tdi => tdi.id === todoitem.value.id);
    todoitems.value[index]=Object.assign(tdi, { id: todoitem.value.id});
    emitter.emit('alert', {
        msg: "Zaaktualizowano zadanie",
        variant: 'info'
    })
    component.value = "list";
  }
  else
  {
    let createdTodoitem = await TodoitemRepository.add(tdi);
    todoitems.value.push(createdTodoitem);
    todoitem.value = createdTodoitem;
    emitter.emit('alert', {
        msg: "Dodano zadanie",
        variant: 'info'
    })
    component.value = "list";
  }
}

const logout = () => auth.logout(router);
</script>

