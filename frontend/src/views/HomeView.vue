<script setup lang="ts">
import AddContact from '@/components/Contacts/AddContact.vue'
import ContactList from '@/components/Contacts/ContactList.vue'
import { isLogged } from '@/helpers/User'
</script>

<template>
  <main>
    <h1 class="p-4">Contact list</h1>
    <div v-if="isLogged">
      <AddContact />
    </div>
    <ContactList />
  </main>
</template>

<style scoped>
h1 {
  text-align: center;
  font-size: 60px;
}
</style>
