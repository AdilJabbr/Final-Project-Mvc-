const headers = document.querySelectorAll('.accordion-header');
headers.forEach(header => {
  header.addEventListener('click', () => {
    const content = header.nextElementSibling;

    // Bütün digər contentləri bağlayırıq
    document.querySelectorAll('.accordion-content').forEach(c => {
      if (c !== content) {
        c.style.maxHeight = null;
        c.classList.remove('open');
      }
    });

    if (content.classList.contains('open')) {
      // Açıq idisə bağla
      content.style.maxHeight = null;
      content.classList.remove('open');
    } else {
      // Bağlı idisə aç
      content.classList.add('open');
      content.style.maxHeight = content.scrollHeight + "px";
    }
  });
});
const cardNumberInput = document.getElementById('card-number');
const cvvInput = document.getElementById('cvv');
const cardNumberPreview = document.getElementById('card-number-preview');
const cvvPreview = document.getElementById('cvv-preview');

cardNumberInput.addEventListener('input', (e) => {
  let value = e.target.value.replace(/\D/g, '').substring(0,16);
  value = value.replace(/(.{4})/g, '$1 ').trim();
  cardNumberInput.value = value;

  cardNumberPreview.textContent = value.padEnd(19, '•');
});

cvvInput.addEventListener('input', (e) => {
  let value = e.target.value.replace(/\D/g, '').substring(0,3);
  cvvInput.value = value;
  cvvPreview.textContent = value.padEnd(3, '•');
});
