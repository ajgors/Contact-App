<script setup lang="ts">
import ContactForm from './ContactForm.vue'
import Modal from '../Modal/Modal.vue'
</script>

<template>
  <div>
    <div class="d-flex justify-content-center mb-3">
      <button
        type="button"
        class="btn btn-success"
        data-bs-toggle="modal"
        data-bs-target="#semesterModal"
      >
        Add new Contact
      </button>
    </div>

    <Modal modal-id="semesterModal">
      <template #header> Add new Contact </template>
      <template #body>
        <ContactForm />
      </template>
    </Modal>
  </div>
</template>
