<template>
  <div class="todo-list">
    <h2 class="header">Lista zadań</h2>

    <div class="filter-date">
      <label for="filter">Pokaż zadania z dnia:</label>
      <input id="filter" type="date" v-model="selectedDate" />
      <button @click="selectedDate = ''" class="clear-btn">Wyczyść filtr</button>
      <div style="width: 20%"></div>
      <button @click="$emit('create')" class="add-btn">Dodaj zadanie</button>
    </div>

    <!-- <div v-if="filteredItems.length === 0" class="empty">
      Brak zadań do wyświetlenia.
    </div> -->

    <div class="items">
      <div v-for="(item, index) in filteredItems" :key="index" class="todo-card">
        <div class="content">
          <h3 class="title">{{ item.title }}</h3>
          <p class="description">{{ item.description }}</p>
          <span class="date">Termin: {{ formatDate(item.date) }}</span>
        </div>
        <div class="actions">
          <button class="edit-btn" @click="$emit('edit', item)">Edytuj</button>
          <button class="delete-btn" @click="$emit('delete', item)">Usuń</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
  todoItems: {
    type: Array,
    required: true
  }
});

defineEmits(['edit', 'delete']);

const selectedDate = ref('');

const filteredItems = computed(() => {
  if (!selectedDate.value) return props.todoItems;
  return props.todoItems.filter(item => {
    const itemDate = new Date(item.date).toISOString().split('T')[0];
    return itemDate === selectedDate.value;
  });
});

const formatDate = (dateStr) => {
  const date = new Date(dateStr);
  return date.toLocaleDateString('pl-PL', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
};
</script>

<style scoped>
.todo-list {
  padding: 24px;
  max-width: 700px;
  margin: 0 auto;
}

.header {
  text-align: center;
  font-size: 24px;
  margin-bottom: 20px;
}

.filter-date {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 16px;
  justify-content: center;
}

.clear-btn {
  background-color: #e5e7eb;
  border: none;
  padding: 6px 10px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 14px;
  transition: 0.2s ease-in-out;
}

.add-btn{
  background-color: #2cea29;
  border: none;
  padding: 6px 10px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 14px;
  transition: 0.2s ease-in-out;
}

.add-btn:hover{
  background-color: #176c09;
}

.clear-btn:hover {
  background-color: #d1d5db;
}

.empty {
  text-align: center;
  color: #777;
  font-style: italic;
}

.items {
  display: grid;
  gap: 16px;
}

.todo-card {
  background-color: #f9fafb;
  border-left: 5px solid #3b82f6;
  padding: 16px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0,0,0,0.05);
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
}

.content {
  flex: 1;
}

.title {
  margin: 0 0 8px;
  font-size: 18px;
  color: #1f2937;
}

.description {
  margin: 0 0 6px;
  color: #4b5563;
}

.date {
  font-size: 14px;
  color: #6b7280;
}

.actions {
  display: flex;
  gap: 8px;
  margin-top: 10px;
}

.edit-btn,
.delete-btn {
  padding: 6px 12px;
  font-size: 14px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s ease-in-out;
}

.edit-btn {
  background-color: #10b981;
  color: white;
}

.edit-btn:hover {
  background-color: #059669;
}

.delete-btn {
  background-color: #ef4444;
  color: white;
}

.delete-btn:hover {
  background-color: #dc2626;
}
</style>
